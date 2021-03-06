﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_PHONE || WINDOWS_PHONE_APP
namespace Accela.WindowsPhone.Sample.Services
#else
namespace Accela.WindowsStore.Sample.Services
#endif
{   /// <summary>
    /// Serialize service interface
    /// </summary>
    public interface ISerializerService
    {
        object Deserialize(Type type, string data);

        T Deserialize<T>(string data);

        string Serialize(object instance);
    }
}
