-- Runs entirely within a transaction

-- Delete everything
DELETE FROM reservation
DELETE FROM site
DELETE FROM campground
DELETE FROM park

-- Insert new stuff
-- For testing purposes, all we need is one park, one campground, one site, maybe three reservations

INSERT INTO park 
	(name, location, establish_date, area, visitors, description)
VALUES 
	('Acadia', 'Maine', '1919-02-26', 47389, 2563129,
	'Covering most of Mount Desert Island and other coastal islands, Acadia features the tallest mountain on the Atlantic coast of the United States, granite peaks, ocean shoreline, woodlands, and lakes. There are freshwater, estuary, forest, and intertidal habitats.');

DECLARE @parkid int = (SELECT @@identity)

INSERT INTO campground (park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (@parkid, 'Blackwoods', 1, 12, 35.00);

DECLARE @campid int = (SELECT @@identity)

INSERT INTO site (site_number, campground_id) VALUES (1, @campid);

DECLARE @siteid int = (SELECT @@identity)

-- Instead of getdates, we'll use constants because then we don't have to guess
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@siteid, 'Smith Family Reservation', '2019-07-05', '2019-07-09');
DECLARE @smithid int = (SELECT @@identity)
INSERT INTO reservation (site_id, name, from_date, to_date) VALUES (@siteid, 'Lockhart Family Reservation', '2019-07-21', '2019-07-24');
DECLARE @lockhartid int = (SELECT @@identity)

SELECT @parkid AS 'Parkid', @campid AS 'Campid', @siteid AS 'Siteid', @smithid AS 'Smith', @lockhartid AS 'Lockhart'