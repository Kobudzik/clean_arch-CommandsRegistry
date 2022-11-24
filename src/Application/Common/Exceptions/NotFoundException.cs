﻿using System;

namespace CommandsRegistry.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" of key \"{key}\" was not found.")
        {
        }
    }
}
