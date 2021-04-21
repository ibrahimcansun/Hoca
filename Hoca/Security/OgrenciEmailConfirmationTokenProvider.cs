using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoca.Security
{
    public class OgrenciEmailConfirmationTokenProvider<TUser> 
        : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public OgrenciEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
                        IOptions<OgrenciEmailConfirmationTokenProviderOptions> options,
                        ILogger<DataProtectorTokenProvider<TUser>> logger) 
            : base(dataProtectionProvider, options, logger)
        {

        }
    }
    
}
