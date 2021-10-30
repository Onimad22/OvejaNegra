using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OvejaNegra.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboProducto();
    }
}