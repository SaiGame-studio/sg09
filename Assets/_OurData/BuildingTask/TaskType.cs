public enum TaskType
{
    none = 0,

    //General
    goToHome = 100,
    goToWorkStation = 101,
    gotoWorkingPoint = 102,
    makingResource = 103,
    bringResourceBack = 104,
    findWorkingPoint = 105,
    getResNeed2Move = 106,

    //Woodcutter
    plantTree = 200,
    chopTree = 201,
    findTree2Chop = 102,//TODO: use findWorkingPoint instead

    //Warehouse
    findBuildingHasProduct = 300,
    gotoGetProduct = 301,
    takingProductBack = 302,
    findBuildingNeedMaterial = 310,
    bringMatetiral2Building = 311,

    //House Builder
    findWarehouseHasRes = 400,
    buildConstruction = 401,
}