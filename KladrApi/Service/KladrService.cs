using System;
using System.Collections.Generic;
using System.Linq;
using KladrApi.Dtos;

namespace KladrApi.Service
{
    public class KladrService
    {
        DataBaseContext _db;

        public KladrService(DataBaseContext db)
        {
            _db = db;
        }
        
        public KladrDto getItem(string id)
        {
            KLADR kladr = _db.KLADR.Single(k => k.Id == id);
            return new KladrDto(kladr.Id, kladr.ParentId, kladr.Name, KladrType.AREA);
        }

        public List<KladrDto> findItems(string query, KladrType? type, string? parentId)
        {
            List<KladrDto> kladrDtos = new List<KladrDto>();
            if (type == KladrType.AREA)
            {
                List<KLADR> resultDb = 
                    query != null ?
                    _db.KLADR.Where(p => p.Name.ToLower().Contains(query.ToLower())
                        && p.ParentId == null
                        )
                    .ToList() :
                    _db.KLADR.Where(k => k.ParentId == null).ToList(); 
                foreach (var kladr in resultDb)
                {
                    kladrDtos.Add(new KladrDto(kladr.Id, kladr.ParentId, kladr.Name, KladrType.AREA));
                }
                return kladrDtos;
            }
            else
            {
                if (type == KladrType.REGION)
                {
                    List<KLADR> resultDb =
                        _db.KLADR
                            .Where(p =>
                                (query == null || p.Name.ToLower().Contains(query.ToLower()))
                                && p.ParentId != null
                                && (parentId == null || p.ParentId == parentId)
                                && p.Id.Length <= 5
                            )
                            .Take(50)
                            .ToList();
                    KLADR parentKladr = null;
                    if (parentId != null)
                    {
                        parentKladr = _db.KLADR.Single(k => k.Id == parentId);
                    }
                    foreach (var kladr in resultDb)
                    {
                        //todo Вынести дозапрос родителя в запрос выше
                        String displayName = null;
                        if (parentKladr == null)
                        {
                            displayName = _db.KLADR.Single(k => k.Id == kladr.ParentId).Name;
                        }
                        else
                        {
                            displayName = parentKladr.Name;
                        }

                        displayName = kladr.Name + " " + displayName;
                        kladrDtos.Add(new KladrDto(kladr.Id, kladr.ParentId, kladr.Name, KladrType.REGION, displayName));
                    }

                    return kladrDtos;
                }
                else
                {
                    if (type == KladrType.CITY)
                    {
                        List<KLADR> resultDb =
                            _db.KLADR
                                .Where(p =>
                                    (query == null || p.Name.ToLower().Contains(query.ToLower()))
                                    && p.ParentId != null
                                    && (parentId == null || p.ParentId == parentId)
                                    && p.Id.Length > 5
                                )
                                .Take(50)
                                .ToList();
                        
                        
                        //todo этот блок перентов так же нужно перенести на запрос
                        KLADR parentRegion = null;
                        KLADR parentArea = null;
                        if (parentId != null)
                        {
                            parentRegion = _db.KLADR.Single(k => k.Id == parentId);
                            if (parentRegion.ParentId != null)
                            {
                                parentArea = _db.KLADR.Single(k => k.Id == parentRegion.ParentId);
                            }
                        }
                        
                        foreach (var kladr in resultDb)
                        {
                            //todo Вынести дозапрос родителя в запрос выше
                            if (parentId == null)
                            {
                                var parents = _db.KLADR.Where(k => k.Id == kladr.ParentId).ToList();
                                parentRegion = parents.Count > 0 ? parents[0] : null;
                                if (parentRegion != null && parentRegion.ParentId != null)
                                {
                                    parents = _db.KLADR.Where(k => k.Id == parentRegion.ParentId).ToList();
                                    parentArea = parents.Count > 0 ? parents[0] : null;
                                }
                            }
                            
                            string displayName = kladr.Name + " " + (parentRegion != null ? parentRegion.Name : " ") + " " +
                                                 (parentArea != null ? parentArea.Name : " ")
                                                 ;
                            kladrDtos.Add(new KladrDto(kladr.Id, kladr.ParentId, kladr.Name, KladrType.CITY, displayName));
                        }

                        return kladrDtos;

                    }
                }
            }

            return null;
        }

    }
    
    
}