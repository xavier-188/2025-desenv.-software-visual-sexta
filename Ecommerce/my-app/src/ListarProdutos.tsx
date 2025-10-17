//Componente
// - Precisa ser uma função
// - Precisa retornar apenas um elemento HTML pai

import { useEffect } from "react";

// - Precisa exportar o componente
function ListarProdutos(){

    //Realiza operações ao carregar o componente
    useEffect(() => {
         console.log("O componente foi carregado");
         buscarProdutosAPI();

    }, []);

    async function buscarProdutosAPI() {
        try {
            const resposta = await fetch("http://localhost:5194/api/produto/listar");
             console.log(resposta);    

             if(!resposta.ok){
                throw new Error("Erro na Requisição: " + resposta.statusText);
             }

               const dados =  await resposta.json();
               console.table(dados);

        } catch (error) {
            console.log("Erro na Requisição: " + error);
        }
    }

    return(
        <div id="listar_produtos">
        <h1>Listar Produtos</h1>
        
        </div>
    );
}

export default ListarProdutos;