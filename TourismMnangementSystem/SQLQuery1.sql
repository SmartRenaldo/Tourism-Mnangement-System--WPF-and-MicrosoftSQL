select tmp.Name as City, t.Name as Tourism from Tourism t
inner join (select c.Name, ctr.TourismId from City c
inner join CityTourismRelationship ctr on c.Id = ctr.CityId) tmp on t.Id = tmp.TourismId;