namespace ZZ_ERP.Infra.CrossCutting.Connections.Commons
{
    public static class ServerCommands
    {
        public static string Login = "Login";
        public static string Logout = "Logout";
        public static string Register = "Register";
        public static string Exit = "Exit";
        public static string LogResultOk = "LogOK";
        public static string LogResultDeny = "LogDeny";
        public static string ClientLogged = "ClientLogged";
        public static string UndefinedCommand = "UndefinedCommand";
        public static string Packages = "Packages";
        public static string EmptyStr = "ACaralhaEstaVazia";
        public static string TimeOut = "TimeOut";
        public static int TimeOutApiRequest = 10000;
        public static string WhatTimeIsIt = "WhatTimeIsIt";
        public static string Wait = "w8";
        public static string HearthBit = "HearthBit";
        public static long HearthBitId = -8765;
        public static string ClientTimeOutQuit = "ClientTimeOutQuit";
        public static string BroadCastId = "All";
        public static string IsUser = "IsUser";
        public static string IsController = "IsController";
        public static string AddClientAuthorized = "AddClientAuthorized";
        public static string RemoveClientAuthorized = "RemoveClientAuthorized";
        public static string RepeatedHumanCode = "RepeatedHumanCode";

        #region Actions
        public static string GetAll = "GetAll";
        public static string GetByHumanCode = "GetByHumanCode";
        public static string GetById = "GetById";
        public static string Add = "Add";
        public static string Edit = "Edit";
        public static string Disable = "Disable";
        public static string UpdateStates = "UpdateStates";
        public static string GetAllStates = "GetAllStates";
        public static string UpdateCities = "UpdateCities";
        public static string GetCityByUf = "GetCityByUf";
        public static string GetAddressByZipCode = "GetAddressByZipCode";
        public static string GetAddress = "GetAddress";
        public static string SaveAddress = "SaveAddress";
        public static string EditAddress = "EditAddress";
        #endregion

        #region Permissions
        public static string Create = "Create";
        public static string Read = "Read";
        public static string Update = "Update";
        public static string Delete = "Delete";
        #endregion

        #region Telas
        public const string TipoServico = "TipoServico";
        public const string UnidadeMedida = "UnidadeMedida";
        public const string TipoOS = "TipoOS";
        public const string CondicaoPagamento = "CondicaoPagamento";
        public const string CentroCustoSintetico = "CentroCustoSintetico";
        public const string TabelaCusto = "TabelaCusto";
        public const string Servico = "Servico";
        public const string Localization = "Localization";
        public const string Funcionario = "Funcionario";
        public const string Fornecedor = "Fornecedor";
        public const string Cliente = "Cliente";
        public const string Estoque = "Estoque";
        public const string TipoEntrada = "TipoEntrada";
        public const string FuncionarioEstoque = "FuncionarioEstoque";
        #endregion

    }
}
