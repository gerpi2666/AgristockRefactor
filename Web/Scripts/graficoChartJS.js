////https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.2.2/Chart.bundle.min.js

function pintarGrafico(canvasId, titulo, tituloEtiquetas, tipo, colores, etiquetas, valores) {
    var data = {
        labels: etiquetas,
        datasets: [{
            label: titulo,
            backgroundColor: colores,
            borderWidth: 2,
            data: valores
        }]
    };

    var ctx = document.getElementById(canvasId).getContext("2d");
    new Chart(ctx, {
        type: tipo,
        data: data,
        options: {
            animation: {
                duration: 1000
            },
            hover: {
                animationDuration: 3000
            },
            responsiveAnimationDuration: 3000,
            legend: { display: true },
            title: {
                display: true,
                text: tituloEtiquetas
            },
            responsive: true,
            maintainAspectRatio: false, // Cambiado a "false" para ajustar el tamaño
        }
    });
}

