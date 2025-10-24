import { useEffect, useState } from "react";
import Produto from "../../../models/Produto";

// Componente
// - HTML, CSS e JS ou TS
// - Regras para ser um componente
// - Precisa ser uma função
// - Precisa retornar apenas um elemento HTML pai

// - Precisa exportar o componente
function ListarProdutos() {
  // Estados - Variáveis
  const [produtos, setProdutos] = useState<Produto[]>([]);

  // Realiza operações ao carregar o componente
  useEffect(() => {
    console.log("O componente foi carregado");
    buscarProdutosAPI();
  }, []);

  async function buscarProdutosAPI() {
    try {
      const resposta = await fetch("http://localhost:5194/api/produto/listar");
      console.log(resposta);

      if (!resposta.ok) {
        throw new Error("Erro na Requisição: " + resposta.statusText);
      }

      const dados = await resposta.json();
      setProdutos(dados);
    } catch (error) {
      console.log("Erro na Requisição: " + error);
    }
  }

  // O return é a parte visual do componente
  return (
    <div id="listar_produtos">
      <h1>Listar Produtos</h1>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Nome</th>
            <th>Descrição</th>
            <th>Preço</th>
            <th>Criado Em</th>
          </tr>
        </thead>
        <tbody>
          {produtos.map((produto) => {
            return (
              <tr key={produto.id}>
                <td>{produto.id}</td>
                <td>{produto.nome}</td>
                <td>{produto.descricao}</td>
                <td>{produto.preco}</td>
                <td>{produto.criadoEm}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default ListarProdutos;