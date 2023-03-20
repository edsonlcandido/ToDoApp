CREATE TABLE Tarefas (
    Id          STRING   PRIMARY KEY
                         UNIQUE
                         NOT NULL,
    Descricao   STRING,
    DataCriacao DATETIME,
    Concluido   BOOLEAN
);
