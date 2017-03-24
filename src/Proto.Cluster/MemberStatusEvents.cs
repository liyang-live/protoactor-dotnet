﻿// -----------------------------------------------------------------------
//   <copyright file="DeadLetter.cs" company="Asynkron HB">
//       Copyright (C) 2015-2017 Asynkron HB All rights reserved
//   </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Proto.Cluster
{
    public class MemberStatus
    {
        public MemberStatus(string memberId, string host, int port, IReadOnlyCollection<string> kinds, bool alive)
        {
            MemberId = memberId ?? throw new ArgumentNullException(nameof(memberId));
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Kinds = kinds ?? throw new ArgumentNullException(nameof(kinds));
            Port = port;
            Alive = alive;
        }

        public string Address => Host + ":" + Port;
        public string MemberId { get; }
        public string Host { get; }
        public int Port { get; }
        public IReadOnlyCollection<string> Kinds { get; }
        public bool Alive { get; }
    }

    public class ClusterTopologyEvent
    {
        public ClusterTopologyEvent(IReadOnlyCollection<MemberStatus> statuses)
        {
            Statuses = statuses ?? throw new ArgumentNullException(nameof(statuses));
        }

        public IReadOnlyCollection<MemberStatus> Statuses { get; }
    }

    public abstract class MemberStatusEvent
    {
        protected MemberStatusEvent(string host, int port, IReadOnlyCollection<string> kinds)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Kinds = kinds ?? throw new ArgumentNullException(nameof(kinds));
            Port = port;
        }

        public string Address => Host + ":" + Port;
        public string Host { get; }
        public int Port { get; }
        public IReadOnlyCollection<string> Kinds { get; }
    }

    public class MemberJoinedEvent : MemberStatusEvent
    {
        public MemberJoinedEvent(string host, int port, IReadOnlyCollection<string> kinds) : base(host, port, kinds)
        {
        }
    }

    public class MemberRejoinedEvent : MemberStatusEvent
    {
        public MemberRejoinedEvent(string host, int port, IReadOnlyCollection<string> kinds) : base(host, port, kinds)
        {
        }
    }

    public class MemberLeftEvent : MemberStatusEvent
    {
        public MemberLeftEvent(string host, int port, IReadOnlyCollection<string> kinds) : base(host, port, kinds)
        {
        }
    }

    public class MemberUnavailableEvent : MemberStatusEvent
    {
        public MemberUnavailableEvent(string host, int port, IReadOnlyCollection<string> kinds)
            : base(host, port, kinds)
        {
        }
    }

    public class MemberAvailableEvent : MemberStatusEvent
    {
        public MemberAvailableEvent(string host, int port, IReadOnlyCollection<string> kinds) : base(host, port, kinds)
        {
        }
    }
}