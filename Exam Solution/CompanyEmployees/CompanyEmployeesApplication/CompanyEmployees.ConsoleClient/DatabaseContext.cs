namespace CompanyEmployees.ConsoleClient
{
    using CompnayEmployees.Data;

    /// <summary>
    /// Middle man...faster change of context in the other classes. Yeah it sucks...but it's an exam...i have to save time.
    /// </summary>
    public class DatabaseContext : CompanyEmployeesEntities
    {
    }
}
