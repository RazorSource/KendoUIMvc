﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonMvc.Razor.Controls
{
    public interface IHtmlForm<TModel> : IForm<TModel, IHtmlForm<TModel>>
    {

    }
}
