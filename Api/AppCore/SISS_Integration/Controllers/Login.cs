using Api.AppCore.API_Common.SeveralFunctions;
using Api.AppCore.SISS_Integration.Models;

namespace Api.AppCore.SISS_Integration.Controllers
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Login
    {
        #region --> Private properties. <--

        private readonly List<ConnectLogin> listConnectLogin = new()
        {
            new()
            {
                Identifier = 1,
                Username = "238fe4eb221c254f2d64ef5696a5dfe8",
                Password = "c82fd41cfd816fa4acb5f0c99b240e25",
                Role = "921130442a68c485734fcaf4ca9dddad",
                Included = Tools.GetDateTimeNow(),
                Count = 1,
                Last = Tools.GetDateTimeNow(),
                Active = true
            }
        };

        #endregion --> Private properties. <--

        #region --> Public methods. <--

        public ConnectLogin Get(ConnectLogin connectLogin, ref string Message)
        {
            List<ConnectLogin> listLogin = listConnectLogin.Where(x => x.Active &&
                                                                       x.Username == Cryptography.DoMD5(connectLogin.Username) &&
                                                                       x.Password == Cryptography.DoMD5(connectLogin.Password))
                                                           .ToList();

            if (listLogin.Count != default)
            {
                if (listLogin.Count == 1)
                {
                    connectLogin = listLogin[0];
                    connectLogin.Count++;
                    connectLogin.Last = Tools.GetDateTimeNow();

                    return connectLogin;
                }
                else Message = "Foi detectada inconsistências neste usuário/senha, sendo assim, não será liberado o acesso.";
            }
            else Message = "Não foi possível localizar este usuário/senha, o mesmo não existe ou está desativado.";

            return default;
        }

        #endregion --> Public methods. <--
    }
}
