$(document).ready(function () {
    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);

    $('.btn-delete').on('click', function (e) {
        if (!confirm('Tem certeza que deseja excluir este item?')) {
            e.preventDefault();
        }
    });

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    $('.money-input').mask('#.##0,00', { reverse: true });
    $('.cpf-input').mask('000.000.000-00');
    $('.date-input').mask('00/00/0000');
});
function formatarMoeda(valor) {
    return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
    }).format(valor);
}

function calcularDiferencaDias(data1, data2) {
    const umDia = 24 * 60 * 60 * 1000;
    const primeiraData = new Date(data1);
    const segundaData = new Date(data2);

    return Math.round((segundaData - primeiraData) / umDia);
}

function validarCPF(cpf) {
    cpf = cpf.replace(/[^\d]+/g, '');

    if (cpf.length !== 11 || /^(\d)\1{10}$/.test(cpf)) {
        return false;
    }

    let soma = 0;
    let resto;

    for (let i = 1; i <= 9; i++) {
        soma += parseInt(cpf.substring(i - 1, i)) * (11 - i);
    }

    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.substring(9, 10))) return false;

    soma = 0;
    for (let i = 1; i <= 10; i++) {
        soma += parseInt(cpf.substring(i - 1, i)) * (12 - i);
    }

    resto = (soma * 10) % 11;
    if (resto === 10 || resto === 11) resto = 0;
    if (resto !== parseInt(cpf.substring(10, 11))) return false;

    return true;
}
function mostrarNotificacao(mensagem, tipo = 'success') {
    const alertaHtml = `
        <div class="alert alert-${tipo} alert-dismissible fade show" role="alert">
            <i class="fas fa-${tipo === 'success' ? 'check-circle' : 'exclamation-circle'} me-2"></i>${mensagem}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;

    $('main.container').prepend(alertaHtml);

    setTimeout(function () {
        $('.alert').fadeOut('slow');
    }, 5000);
}

function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}