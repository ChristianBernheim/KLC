//hämta html element
const canvas = document.querySelector("#advanced-chart");

//sätt höjd bredd på canvas
canvas.width = 500;
canvas.height = 400;

//hämta canvas kontext
const ctx = canvas.getContext("2d");

//importera data från api
    const mChart = async (id) => {
    const response = await fetch("https://informatik13.ei.hv.se/KLCAPI/api/MatningNews2/GetAllFromPatient/" + id);
    const mdata = await response.json();
    console.log(mdata);
  

    //värden som kommer återanvändas
    let mBorderwidth = 8;
    let mHoverBorderwidth = 30;
    let mTension = 0.2;

    //skapa chart
    new Chart(ctx, {
        type: "line",
        data: {
            //använder counts funktionen ovan
            //kommer bli ytterligare en data.map sen
            labels: mdata.map((value) => {
                let date = new Date(Date.parse(value.tidForMatning));
                return date.toUTCString().split(",")[0] + " " + date.getHours() + ":" + date.getMinutes();
            }),
                datasets: [
                {
                    label: "Andningsfrekvens",
                    //hämtar alla värden ur data arrayen, för key "andningsfrekvens"
                    data: mdata.map((value) => Math.abs(value.andningsfrekvens)),
                    //Inställningar för chart, ex. hur mycket bezier curve man vill ha
                    borderColor: "#488f31",
                    backgroundColor: "#488f31",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
                {
                    label: "Syremättnad",
                    data: mdata.map((value) => {
                        if (value != null) { 
                        if (value.syreMattnad >= 7) {
                            value.syreMattnad = value.syreMattnad - 10;
                        }
                            return Math.abs(value.syreMattnad);
                        }
                    }),
                    borderColor: "#8aac49",
                    backgroundColor: "#8aac49",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
                {
                    label: "Tillförd syrgas",
                    data: mdata.map((value) => { if (value == null) { return null} else return Math.abs(value.tillfordSyrgas) } ),
                    borderColor: "#c6c96a",
                    backgroundColor: "#c6c96a",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
                {
                    label: "Systoliskt blodtryck",
                    data: mdata.map((value) => { if (value == null) { return -1 } else return Math.abs(value.systolisktBlodtryck) }),
                    borderColor: "#ffe792",
                    backgroundColor: "#ffe792",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
                {
                    label: "Pulsfrekvens",
                    data: mdata.map((value) => { if (value == null) { return -1 } else { return Math.abs(value.pulsfrekvens) } }),
                    borderColor: "#f8b267",
                    backgroundColor: "#f8b267",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
                {
                    label: "Medvetandegrad",
                    data: mdata.map((value) => { if (value != null) { return Math.abs(value.medvetandegrad) } }),
                    borderColor: "#eb7a52",
                    backgroundColor: "#eb7a52",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
                {
                    label: "Temperatur",
                    data: mdata.map((value) => { if (value != null) { return Math.abs(value.temperatur) } }),
                    borderColor: "#de425b",
                    backgroundColor: "#de425b",
                    borderWidth: mBorderwidth,
                    tension: mTension,
                    pointHoverRadius: mHoverBorderwidth,
                },
            ],
  },
        options: {
            scales: {
                y: {
                    min: 0, // Minsta värde
                    max: 4, // Högsta värde

                    ticks: {
                        stepSize: 1, // steg mellan värden
                        // Sätt de värden vi vill ha på Y-axis
                        callback: function (value, index, values) {
                            var labels = [
                                "0",
                                "1",
                                "2",
                                "3",
                                "",
                            ];

                            return labels[index];
                        },
                    },
                },
            },
        },
    });
}

export default mChart;
