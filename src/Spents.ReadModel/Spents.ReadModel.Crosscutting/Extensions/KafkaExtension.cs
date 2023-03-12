﻿using KafkaFlow;
using KafkaFlow.Admin.Dashboard;
using KafkaFlow.Configuration;
using KafkaFlow.Serializer;
using KafkaFlow.TypedHandler;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Spents.ReadModel.Application.Kafka.Handlers;
using Spents.ReadModel.Application.Kafka.KafkaBus;
using Spents.ReadModel.Application.Kafka.Middlewares;
using Spents.ReadModel.Crosscutting.Models;
using Spents.Topics;

namespace Spents.ReadModel.Crosscutting.Extensions
{
    public static class KafkaExtension
    {
        public static IApplicationBuilder ShowKafkaDashboard(this IApplicationBuilder app) => app.UseKafkaFlowDashboard();

        public static IServiceCollection AddKafka(this IServiceCollection services, KafkaSettings kafkaSettings)
        {
            services.AddKafka(kafka => kafka
                .UseConsoleLog()
                .AddCluster(cluster => cluster
                    .AddBrokers(kafkaSettings)
                    .AddTelemetry()
                    .AddConsumers(kafkaSettings)
                    )
                );
            services.AddHostedService<KafkaBusHostedService>();
            return services;
        }

        private static IClusterConfigurationBuilder AddTelemetry(
            this IClusterConfigurationBuilder builder)
        {
            builder
                .EnableAdminMessages(KafkaTopics.Events.ReceiptTelemetry)
                .EnableTelemetry(KafkaTopics.Events.ReceiptTelemetry);

            return builder;
        }

        private static IClusterConfigurationBuilder AddBrokers(
            this IClusterConfigurationBuilder builder,
            KafkaSettings settings)
        {
            if (settings.Sasl_Enabled)
            {
                builder
                    .WithBrokers(settings.Sasl_Brokers)
                    .WithSecurityInformation(si =>
                    {
                        si.SecurityProtocol = SecurityProtocol.SaslSsl;
                        si.SaslUsername = settings.Sasl_UserName;
                        si.SaslPassword = settings.Sasl_Password;
                        si.SaslMechanism = SaslMechanism.Plain;
                        si.SslCaLocation = string.Empty;
                    });
            }
            else
            {
                builder.WithBrokers(new[] { settings.Brokers });
            }

            return builder;
        }

        private static IClusterConfigurationBuilder AddConsumers(
            this IClusterConfigurationBuilder builder,
            KafkaSettings settings)
        {
            builder.AddConsumer(
                consumer => consumer
                     .Topics(KafkaTopics.Events.Receipt)
                     .WithGroupId("Receipts")
                     .WithName("Receipt-Events")
                     .WithBufferSize(settings.BufferSize)
                     .WithWorkersCount(settings.WorkerCount)
                     .WithAutoOffsetReset(AutoOffsetReset.Latest)
                     .AddMiddlewares(
                        middlewares =>
                            middlewares
                            .AddSerializer<JsonCoreSerializer>()
                            .Add<ConsumerLoggingMiddleware>()
                            .Add<ConsumerRetryMiddleware>()
                            .AddTypedHandlers(
                                h => h
                                    .WithHandlerLifetime(InstanceLifetime.Scoped)
                                    .AddHandler<ReceiptCreatedHandler>()
                                    )
                            )
                     );

            return builder;
        }
    }
}
