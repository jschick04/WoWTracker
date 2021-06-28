﻿using System;

namespace Tracker.Api.Settings {

    public class JwtSettings {

        public string Secret { get; set; }

        public TimeSpan TokenLifetime { get; set; }

        public TimeSpan RefreshTokenLifetime { get; set; }

    }

}