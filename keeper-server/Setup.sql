use ckeeper;
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );


-- CREATE TABLE vaults
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255) NOT NULL,
--   private TINYINT NOT NULL DEFAULT 0,

--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );
-- DROP TABLE vaults;
ALTER TABLE vaults 
ALTER COLUMN description LONGTEXT;
-- ALTER TABLE vaults DROP COLUMN isPrivate;
-- ALTER TABLE vaults ADD isPrivate TINYINT NOT NULL;
-- DELETE FROM vaults;

-- ALTER TABLE vaults CHANGE private isPrivate TINYINT;
-- INSERT INTO vaults
-- (creatorId,name,description,isprivate)
-- VALUES
-- (1,"ss","sddd");
-- SELECT * FROM vaults;
-- ALTER TABLE keeps
-- DROP CONSTRAINT UNIQUE;
-- CREATE TABLE keeps
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   description VARCHAR(255) NOT NULL,
--   img VARCHAR(255) NOT NULL,
--   views INT,
--   shares INT,
--   keeps INT,

--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE
-- );
-- DELETE FROM keeps;

-- CREATE TABLE vaultkeeps
-- (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   vaultId INT NOT NULL,
--   keepId INT NOT NULL,

--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles (id)
--    ON DELETE CASCADE,
  
--   FOREIGN KEY (vaultId)
--    REFERENCES vaults (id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (keepId)
--    REFERENCES keeps (id)
--    ON DELETE CASCADE
-- );

 

