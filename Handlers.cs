using System;
using System.Threading;
using System.Threading.Tasks;
using bot.Entity;
using bot.HttpClients;
using bot.Services;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using bot.Dto.V2;

namespace bot
{
    public partial class Handlers
    {
        private readonly ILogger<Handlers> _logger;
        private readonly IStorageService _storage;
        private readonly ICacheService _cache;

        public Handlers(
            ILogger<Handlers> logger, 
            IStorageService storage,
            ICacheService cache)
        {
            _logger = logger;
            _storage = storage;
            _cache = cache;
        }
    }
}