-- Desafio  2:  O  Gerente  financeiro  requisitou  junto  a  Globaltec,  uma  consulta  que  traga  as informações de contas a pagar e contas pagas, sendo que ele precisa do número do processo de pagamento, nome do fornecedor, data de vencimento, data de pagamento, valor líquido e um identificador se é conta a pagar ou paga. 

SELECT  ca.Numero AS [Numero do Processo], p.Nome AS [Nome do Fornecedor], ca.DataVencimento, null AS [Data do Pagamento], ROUND((Valor + Acrescimo) - Desconto, 2 ) AS [Valor Líquido], 'PENDENTE' AS Situacao
FROM Pessoas p, ContasAPagar ca
WHERE p.Codigo = ca.CodigoFornecedor

UNION ALL

select  cp.Numero as [Numero do Processo], p.Nome AS [Nome do Fornecedor], cp.DataVencimento, cp.DataPagamento AS [Data do Pagamento], ROUND((Valor + Acrescimo) - Desconto, 2 ) as [Valor Líquido], 'PAGA' AS Situacao
from Pessoas p, ContasPagas cp
Where p.Codigo = cp.CodigoFornecedor