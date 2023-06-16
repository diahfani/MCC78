/*using WebAPI.ViewModels.Others;*/
using Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.Repositories.Interface
{
    public interface IRepository<T, X>
        where T : class
    {
        Task<ResponseListVM<T>> Get();
        Task<ResponseVM<T>> Get(X guid);
        Task<ResponseMessageVM> Post(T entity);
        Task<ResponseMessageVM> Put(T entity);
        Task<ResponseMessageVM> Delete(X guid);
    }
}