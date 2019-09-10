using System;
using System.Web;
using NHibernate;
using NHibernate.Mapping.Attributes;
using NHibernate.Cfg;
using System.IO;

namespace Jieshai.Data
{
    public class NHibernateHelper
    {
        public static ISessionFactory sessionFactory;

        public static string ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NH.config");

        static NHibernateHelper()
        {
            Configuration config = new NHibernate.Cfg.Configuration().Configure(ConfigFilePath);
            using (var stream = new MemoryStream())
            {
                HbmSerializer.Default.Serialize(stream, System.Reflection.Assembly.Load("Jieshai.Data"));

                stream.Position = 0;
                config.AddInputStream(stream);
            }

            sessionFactory = config.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}
