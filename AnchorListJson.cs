
public class Rootobject
{
    public int status { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
}

public class Data
{
    public int page { get; set; }
    public int pageSize { get; set; }
    public int totalPage { get; set; }
    public int totalCount { get; set; }
    public Data1[] datas { get; set; }
    public int time { get; set; }
}

public class Data1
{
    public string gameFullName { get; set; }
    public string gameHostName { get; set; }
    public object boxDataInfo { get; set; }
    public string totalCount { get; set; }
    public string roomName { get; set; }
    public string bussType { get; set; }
    public string screenshot { get; set; }
    public string privateHost { get; set; }
    public string nick { get; set; }
    public string avatar180 { get; set; }
    public string gid { get; set; }
    public string introduction { get; set; }
    public string recommendStatus { get; set; }
    public string recommendTagName { get; set; }
    public string isBluRay { get; set; }
    public string bluRayMBitRate { get; set; }
    public string screenType { get; set; }
    public string liveSourceType { get; set; }
    public string uid { get; set; }
    public string channel { get; set; }
    public string liveChannel { get; set; }
    public object imgRecInfo { get; set; }
    public string aliveNum { get; set; }
    public Attribute attribute { get; set; }
    public string profileRoom { get; set; }
    public string isRoomPay { get; set; }
    public string roomPayTag { get; set; }
    public int isWatchTogetherVip { get; set; }
}

public class Attribute
{
    public Listpos1 ListPos1 { get; set; }
    public Listpos2 ListPos2 { get; set; }
}

public class Listpos1
{
    public string sContent { get; set; }
    public string sIcon { get; set; }
}

public class Listpos2
{
    public string sContent { get; set; }
    public string sIcon { get; set; }
}
