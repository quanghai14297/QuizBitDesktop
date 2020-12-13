namespace ClientApp
{
    /// <summary>
    /// Loại mặt hàng
    /// </summary>
    public enum EnumInventoryItemType
    {
        [Description("Mặt hàng khác")]
        Other = 0,

        [Description("Món ăn")]
        Food = 1,

        [Description("Đồ uống")]
        Drink = 2
    }

    /// <summary>
    /// Cơ cấu bữa ăn
    /// </summary>
    public enum EnumCourseType
    {
        [Description("Khác")]
        Other = 0,

        [Description("Món khai vị")]
        Starter = 1,

        [Description("Món chính")]
        MainCourse = 2,

        [Description("Món tráng miệng")]
        Desserts = 3

        //[Description("Món đi kèm")]
        //SideDish = 4
    }
}
