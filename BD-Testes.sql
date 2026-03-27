USE Cofrinho;

-- Criar umas pessoas
INSERT INTO pessoas (nome, idade) VALUES
	('Abelardo', 10), ('Bernardo', 32), ('Claudia', 30);

-- Criar umas categorias
INSERT INTO categorias (descricao, finalidade) VALUES
	('Salários', 1), ('Serviços', 0), ('Desp. básicas', -1);
    
-- Criar umas transações
INSERT INTO transacoes (descricao, valor, id_categoria, id_pessoa) VALUES
	('Escola Abelardo', 100, 3, 1), ('Conserto Carro', 1000, 2, 3),
    ('Salário Bernardo', 3000, 1, 2), ('Salário Claudia', 2000, 1, 3);