﻿/*
 * Copyright (c) 2013-2015, SteamDB. All rights reserved.
 * Use of this source code is governed by a BSD-style license that can be
 * found in the LICENSE file.
 */
namespace SteamDatabaseBackend
{
    struct App
    {
        public uint AppID { get; set; }
        public uint AppType { get; set; } /* Not EAppType!! */
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string LastKnownName { get; set; }
    }
}
