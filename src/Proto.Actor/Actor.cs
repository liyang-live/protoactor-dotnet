﻿// -----------------------------------------------------------------------
//   <copyright file="Actor.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Proto
{
    public delegate Task Receive(IContext context);

    public delegate Task Sender(ISenderContext ctx, PID target, MessageEnvelope envelope);

    class EmptyActor : IActor
    {
        private readonly Receive _receive;
        public EmptyActor(Receive receive) => _receive = receive;
        public Task ReceiveAsync(IContext context) => _receive(context);
    }

    public static class Actor
    {
        public static readonly Task Done = Task.FromResult(0);
        public static EventStream EventStream => EventStream.Instance;
        public static Props FromProducer(Func<IActor> producer) => new Props().WithProducer(producer);
        public static Props FromFunc(Receive receive) => FromProducer(() => new EmptyActor(receive));

        public static PID Spawn(Props props)
        {
            var name = ProcessRegistry.Instance.NextId();
            return SpawnNamed(props, name);
        }

        public static PID SpawnPrefix(Props props, string prefix)
        {
            var name = prefix + ProcessRegistry.Instance.NextId();
            return SpawnNamed(props, name);
        }

        public static PID SpawnNamed(Props props, string name)
        {
            return props.Spawn(name, null);
        }
    }

    public class ProcessNameExistException : Exception
    {
        public ProcessNameExistException(string name) : base($"a Process with the name '{name}' already exists")
        {
        }
    }

    public interface IActor
    {
        Task ReceiveAsync(IContext context);
    }
}