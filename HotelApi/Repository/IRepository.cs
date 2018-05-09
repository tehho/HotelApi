﻿using System.Collections.Generic;

namespace HotelApi.DbManager
{
    public interface IRepository<T>
    {
        T Add(T obj);
        T Get(T obj);
        T Update(T obj);
        T Remove(T obj);
        List<T> GetAll();
        List<T> Search(T obj);
    }
}