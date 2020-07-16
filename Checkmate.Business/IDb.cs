using Checkmate.Data;
using System;

namespace Checkmate.Business
{
    public interface IDb
    {
        AppDataDb db { get; }
    }
}
