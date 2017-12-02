﻿using System;

using Stove.Domain.Entities;
using Stove.Domain.Entities.Auditing;

namespace Stove.EntityFrameworkCore.Dapper.Tests.Domain
{
    public class Blog : AggregateRoot, IHasCreationTime
    {
        public Blog()
        {
            Register<BlogUrlChangedEventData>(@event => { Url = @event.Url; });
        }

        public Blog(string name, string url)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            Name = name;
            Url = url;
        }

        public string Name { get; set; }

        public string Url { get; protected set; }

        public DateTime CreationTime { get; set; }

        public void ChangeUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException(nameof(url));
            }

            ApplyChange(new BlogUrlChangedEventData(this, url));
        }
    }
}
