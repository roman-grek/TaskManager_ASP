using TaskManager.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TaskManager.Data
{
    public class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Boards.Any())
            {
                context.Boards.AddRange(Boards.Select(c => c.Value));
            }

            if (!context.Tables.Any())
            {
                context.Tables.AddRange(Tables.Select(c => c.Value));
            }

            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(
                    new Task() {
                        Title = "Card1",
                        Description = "Description1"
                    },
                    new Task() {
                        Title = "Card2",
                        Description = "Description2"
                    },
                    new Task() {
                        Title = "Card3",
                        Description = "Description3"
                    },
                    new Task() {
                        Title = "Card4",
                        Description = "Description4"
                    }
                );
            }
            context.SaveChanges();
            context.Database.EnsureCreated();
        }

        private static Dictionary<string, Board> boards;
        public static Dictionary<string, Board> Boards
        {
            get
            {
                if (boards == null)
                {
                    var boardsList = new Board[]
                    {
                        new Board { Title = "Test1", Description = "Description1", UserId="8b9d829d-0912-4715-862f-35928d229d40" },
                        new Board { Title = "Test2", Description = "Description2", UserId="8b9d829d-0912-4715-862f-35928d229d40" },
                    };

                    boards = new Dictionary<string, Board>();

                    foreach (Board board in boardsList)
                    {
                        boards.Add(board.Title, board);
                    }
                }

                return boards;
            }
        }

        private static Dictionary<string, Table> tables;
        public static Dictionary<string, Table> Tables
        {
            get
            {
                if (tables == null)
                {
                    var tablesList = new Table[]
                    {
                        new Table { Title = "TestTable1", Created = DateTime.Now, Updated = DateTime.Now, Board = Boards["Test1"] },
                        new Table { Title = "TestTable2", Created = DateTime.Now, Updated = DateTime.Now, Board = Boards["Test1"] },
                        new Table { Title = "TestTable3", Created = DateTime.Now, Updated = DateTime.Now, Board = Boards["Test2"] },
                    };

                    tables = new Dictionary<string, Table>();

                    foreach (Table table in tablesList)
                    {
                        tables.Add(table.Title, table);
                    }
                }

                return tables;
            }
        }
    }
}