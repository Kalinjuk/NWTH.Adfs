﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ADFS.APIKeyAuth
{
    public class ApiKey
    {
        public ApiKey(int id, string owner, string key, DateTime created, IReadOnlyCollection<string> roles)
        {
            Id = id;
            Owner = owner ?? throw new ArgumentNullException(nameof(owner));
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Created = created;
            Roles = roles ?? new List<string>();
        }

        public int Id { get; }
        public string Owner { get; }
        public string Key { get; }
        public DateTime Created { get; }
        public IReadOnlyCollection<string> Roles { get; }
    }
}
