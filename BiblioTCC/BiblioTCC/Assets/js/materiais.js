function AbrirModal(titulo, mensagem) {
    document.getElementById("modal").classList.add("active");
    document.getElementById("tituloModal").innerText = titulo;
    document.getElementById("mensagemModal").innerText = mensagem;
}
function FecharModal() {
    document.getElementById("modal").classList.remove("active");
}

window.onload = function () {
    AlterarCorTituloDaPagina();
    AlterarCorMenuLateral();
}

function AlterarCorTituloDaPagina() {
    let tituloDaPagina = document.querySelector("#form1 > nav > div.nav-list > ul > li:nth-child(4) > a");
    tituloDaPagina.style.color = "#FFD369";
    tituloDaPagina.style.borderBottom = "3px solid #FFD369";
}

function AlterarCorMenuLateral() {
    let menuLateral = document.querySelector("div.area > nav > ul > li:nth-child(4) > a");
    menuLateral.style.color = "#FFD369";
}