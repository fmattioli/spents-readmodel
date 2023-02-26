﻿using KafkaFlow;
using Newtonsoft.Json;
using Serilog;
using Spents.ReadModel.Application.Kafka.Extensions;
using System.Diagnostics;

namespace Spents.ReadModel.Application.Kafka.Middlewares
{
    public class ConsumerLoggingMiddleware : IMessageMiddleware
    {
        private readonly ILogger log;

        public ConsumerLoggingMiddleware(ILogger log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task Invoke(IMessageContext context, MiddlewareDelegate next)
        {
            var sw = Stopwatch.StartNew();

            this.log.Information(
                $"[{nameof(ConsumerLoggingMiddleware)}] - Kafka message received.",
                () => new
                {
                    context.ConsumerContext.GroupId,
                    context.ConsumerContext.Topic,
                    PartitionNumber = context.ConsumerContext.Partition,
                    PartitionKey = context.GetPartitionKey(),
                    Headers = context.Headers.ToJsonString(),
                    MessageType = context.Message.Value.GetType().FullName,
                    Message = JsonConvert.SerializeObject(context.Message)
                });

            try
            {
                await next(context);

                this.log.Information(
                    $"[{nameof(ConsumerLoggingMiddleware)}] - Kafka message processed.",
                    () => new
                    {
                        context.ConsumerContext.WorkerId,
                        context.ConsumerContext.GroupId,
                        context.ConsumerContext.Topic,
                        PartitionNumber = context.ConsumerContext.Partition,
                        PartitionKey = context.GetPartitionKey(),
                        context.ConsumerContext.Offset,
                        Headers = context.Headers.ToJsonString(),
                        MessageType = context.Message.Value.GetType().FullName,
                        Message = JsonConvert.SerializeObject(context.Message),
                        ProcessingTime = sw.ElapsedMilliseconds
                    });
            }
            catch (Exception ex)
            {
                this.log.Error(
                    $"[{nameof(ConsumerLoggingMiddleware)}] - Failed to process message.",
                    ex,
                    () => new
                    {
                        context.ConsumerContext.WorkerId,
                        context.ConsumerContext.GroupId,
                        context.ConsumerContext.Topic,
                        PartitionNumber = context.ConsumerContext.Partition,
                        PartitionKey = context.GetPartitionKey(),
                        context.ConsumerContext.Offset,
                        Headers = context.Headers.ToJsonString(),
                        MessageType = context.Message.Value.GetType().FullName,
                        Message = JsonConvert.SerializeObject(context.Message),
                        ProcessingTime = sw.ElapsedMilliseconds
                    });
            }
        }
    }
}
