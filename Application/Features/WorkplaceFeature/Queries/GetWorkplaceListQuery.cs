﻿using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CountryCQ
{
    public class GetWorkplaceListQueryRequest : IRequest <GetWorkplaceListQueryResponse>
    {
    }

    public class GetWorkplaceListQueryHandler : IRequestHandler<GetWorkplaceListQueryRequest, GetWorkplaceListQueryResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkplaceListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<GetWorkplaceListQueryResponse> Handle(GetWorkplaceListQueryRequest query, CancellationToken cancellationToken)
        {
            var workplaces = await _context.Workplaces
                .ProjectTo<WorkplaceDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new GetWorkplaceListQueryResponse
            { 
                Results = workplaces
            };  
            
        }
    }

    public class GetWorkplaceListQueryResponse
    {
        public IEnumerable<WorkplaceDto> Results { get; set; }
    }
    public class WorkplaceDto
    {
        public int Id { get; set; }
        public int WorkplaceNumber { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
    }
}