﻿select * from Tourism t inner join CityTourismRelationship ctr on t.Id = ctr.TourismId where ctr.CityId = @CityId