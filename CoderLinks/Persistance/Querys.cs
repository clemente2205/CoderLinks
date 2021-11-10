using CoderLinks.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderLinks.Persistance
{
    public class Querys
    {
        #region Singleton
        private Querys() { }
        private static Querys instance = null;
        public static Querys Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Querys();
                }
                return instance;
            }
        }

        #endregion Singleton

        public List<WarLog> LogHistory()
        {
            try
            {
                var context = new dbsgdlContext();
                return context.WarLogs.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveWinner(string player, int round)
        {
            try
            {
                using (var contex = new dbsgdlContext())
                {
                    var log = new WarLog()
                    {
                        WinPlayer = player,
                        RoundsToWin = round
                    };

                    contex.Entry(log).State = EntityState.Added;
                    contex.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
