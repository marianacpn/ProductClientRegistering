using Application.App.Interface;
using System;
using AutoMapper;

namespace Application.App.Core
{
    public class AppBase : IAppBase
    {
        protected readonly IMapper _mapper;
        private bool _disposed;

        public AppBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Dispose(bool disposing)
        {
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
