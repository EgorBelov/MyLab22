CREATE TABLE person (
id INTEGER NOT NULL identity PRIMARY KEY,
name TEXT,
age INTEGER,
gender TEXT
);

CREATE TABLE rent (
id INTEGER NOT NULL identity PRIMARY KEY,
person_id INTEGER REFERENCES person(id) ON DELETE CASCADE,
name TEXT
);

CREATE TABLE item (
id INTEGER NOT NULL identity PRIMARY KEY,
rent_id INTEGER REFERENCES rent(id) ON DELETE CASCADE,
name TEXT
);

INSERT INTO person (name, age, gender) VALUES
('John Doe', 30, 'Male'),
('Jane Smith', 25, 'Female');

-- Insert some sample data into the rent table
INSERT INTO rent (person_id, name) VALUES
(1, 'Rent 1'),
(1, 'Rent 2'),
(2, 'Rent 3');

-- Insert some sample data into the item table
INSERT INTO item (rent_id, name) VALUES
(1, 'Item 1'),
(1, 'Item 2'),
(1, 'Item 3'),
(2, 'Item 4'),
(3, 'Item 5'),
(3, 'Item 6');
