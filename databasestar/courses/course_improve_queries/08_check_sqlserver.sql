/*
Lesson 08
SQL Server
*/

ALTER TABLE person
ADD active_status VARCHAR(10);

UPDATE person
SET active_status = 'Active'
WHERE person_id = 2;

UPDATE person
SET active_status = 'Inactive'
WHERE person_id = 3;

UPDATE person
SET active_status = 'Old'
WHERE person_id = 5;

ALTER TABLE person
ADD CONSTRAINT ck_person_status
CHECK (active_status IN ('Active', 'Inactive'));

UPDATE person
SET active_status = 'Inactive'
WHERE person_id = 5;

/*
Will show an error
*/
UPDATE person
SET active_status = 'Old'
WHERE person_id = 6;