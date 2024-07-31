namespace Api.AppCore.Webhook.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class Collaborator
    {
        #region --> Public properties. <--      

        public CollaboratorInfo LuizLenire { get; set; }

        #endregion --> Public properties. <--

        #region --> Constructors. <--

        public Collaborator()
        {
            LuizLenire = new()
            {
                Name = "Luiz",
                Email = "luizlenire@outlook.com"
            };
        }

        #endregion --> Constructors. <--
    }
}
