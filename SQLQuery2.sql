-- создание таблицы "Человек"
CREATE TABLE person (
  id  INTEGER NOT NULL identity PRIMARY KEY,
  name TEXT,
  age INTEGER,
  gender TEXT
);

-- создание таблицы "Приемы пищи"
CREATE TABLE meals (
  id INTEGER NOT NULL identity PRIMARY KEY,
  person_id INTEGER REFERENCES person(id) ON DELETE CASCADE,
  meal_time TEXT
);

-- создание таблицы "Блюда"
CREATE TABLE dishes (
  id INTEGER NOT NULL identity PRIMARY KEY,
  meal_id INTEGER REFERENCES meals(id) ON DELETE CASCADE,
  name TEXT
);

-- создание таблицы "Продукты"
CREATE TABLE products (
  id INTEGER NOT NULL identity PRIMARY KEY,
  dish_id INTEGER REFERENCES dishes(id) ON DELETE CASCADE,
  name TEXT,
  quantity INTEGER
);



INSERT INTO person (name, age, gender) VALUES
('John', 30, 'male'),
('Jane', 25, 'female');



INSERT INTO meals (person_id, meal_time) VALUES
( 1, 'breakfast'),
( 1, 'lunch'),
( 1, 'dinner'),
( 2, 'breakfast'),
( 2, 'lunch'),
( 2, 'dinner');

INSERT INTO dishes (meal_id, name) VALUES
( 1, 'Omelette'),
( 1, 'Toast'),
( 2, 'Chicken sandwich'),
( 2, 'Chips'),
( 3, 'Spaghetti bolognese'),
( 3, 'Garlic bread'),
( 4, 'Porridge'),
( 4, 'Fruit salad'),
( 5, 'Grilled salmon'),
( 5, 'Green beans'),
( 6, 'Beef stir-fry'),
( 6, 'Rice');

INSERT INTO products (dish_id, name, quantity) VALUES
( 1, 'Eggs', 2),
( 1, 'Cheese', 50),
( 2, 'Bread', 2),
( 2, 'Butter', 20),
( 3, 'Chicken breast', 1),
( 3, 'Bread', 2),
( 4, 'Potatoes', 200),
( 4, 'Oil', 30),
( 5, 'Spaghetti', 200),
( 5, 'Beef mince', 200),
( 6, 'Bread', 2),
( 6, 'Garlic', 2),
( 7, 'Oats', 100),
( 7, 'Milk', 200),
( 8, 'Apple', 1),
( 8, 'Banana', 1),
( 9, 'Salmon fillet', 1),
( 9, 'Green beans', 150),
( 10, 'Salmon fillet', 1),
( 10, 'Green beans', 150),
( 11, 'Beef strips', 200),
( 11, 'Vegetables', 200),
( 12, 'Rice', 1),
( 12, 'Water', 0);




select  meals.meal_time, meals.id from meals
where meals.person_id = 1;

select dishes.name, dishes.id from dishes
where dishes.meal_id = 1;

select products.name, products.id from products
where products.dish_id = 1;

delete from person where id = 1

INSERT INTO person (name, age, gender) VALUES ('@name', 20, 'qwe');


SELECT * FROM person

UPDATE person SET name = 'qweName' WHERE person.id = 3