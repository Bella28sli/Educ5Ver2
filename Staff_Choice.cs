//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Educ5Ver2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff_Choice
    {
        public int ID_Staff_Choice { get; set; }
        public int Clients_ID { get; set; }
        public int Employee_ID { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
