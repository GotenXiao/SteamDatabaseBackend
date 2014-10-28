﻿/*
 * Copyright (c) 2013-2015, SteamDB. All rights reserved.
 * Use of this source code is governed by a BSD-style license that can be
 * found in the LICENSE file.
 */
using System;
using MySql.Data.MySqlClient;

namespace SteamDatabaseBackend
{
    class BlogCommand : Command
    {
        public BlogCommand()
        {
            Trigger = "!blog";
        }

        public override void OnCommand(CommandArguments command)
        {
            using (var reader = DbWorker.ExecuteReader("SELECT `ID`, `Slug`, `Title` FROM `Blog` WHERE `IsHidden` = 0 ORDER BY `ID` DESC LIMIT 1"))
            {
                if (reader.Read())
                {
                    var slug = reader.GetString("Slug");

                    if (slug.Length == 0)
                    {
                        slug = reader.GetString("ID");
                    }

                    CommandHandler.ReplyToCommand(
                        command,
                        "Latest blog post:{0} {1}{2} -{3} {4}",
                        Colors.BLUE, reader.GetString("Title"), Colors.NORMAL,
                        Colors.DARKBLUE, SteamDB.GetBlogURL(slug)
                    );
                }
            }
        }
    }
}
