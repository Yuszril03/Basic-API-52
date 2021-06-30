////console.log("sss");

    //$.ajax({
    //    url: "https://pokeapi.co/api/v2/pokemon"
    //}).done((result) => {
    //    console.log(result);
    //    text = "";
    //    no = 1;
    //    $.each(result.results, function (key, val) {
    //        //text += "<li>" + val.name + "</li>";
    //        text += `<tr>
    //                    <td>${no++}</td>
    //                    <td>${val.name}</td>
    //                    <td>${val.url}</td>
    //                    <td>
    //                        <button class="btn btn-primary m-2 " data-toggle="modal" data-target="#exampleModal">Detail</button>
    //                    </td>
    //                 </tr>`;
    //    })
    //    $("#showData").html(text);
    //}).fail((error) => {
    //    console.log(error);
    //});

$(document).ready(function () {
    var t = $('#table').DataTable();
    $.ajax({
        url: "https://pokeapi.co/api/v2/pokemon"
    }).done((result) => {
        console.log(result);
        text = "";
        no = 1;
        $.each(result.results, function (key, val) {
            //text += "<li>" + val.name + "</li>";
            t.row.add([
                no,
                val.name,
                `<a href="${val.url}"  class="btn btn-light">${val.url}</a>`,
                `<button class="btn btn-primary m-2 open"  data-id="${key}" data-toggle="modal" data-target="#exampleModal">Detail</button>`
            ]).draw(false)
            no++;
        })
    }).fail((error) => {
        console.log(error);
    });

    $(document).on("click", ".open", function () {
        var myBookId = $(this).data('id')+1;
        $.ajax({
            url: 'https://pokeapi.co/api/v2/pokemon/' + myBookId
        }).done((result) => {
            //console.log(result);

            $("#imgFoto").html('<img class=" w-50" src="' + result.sprites.other.dream_world.front_default+'" alt="" />');
            $("#exampleModalLabel").html("Detail " + result.name);
            $("#namePoke").html(result.name);
            $("#heightPoke").html(result.height+" cm");
            $("#weightPoke").html(result.weight + " Kg");
            hslABS = "";
            $.each(result.abilities, function (key, val) {
                hslABS += `<span class="badge badge-primary">${val.ability.name}</span> `;
            })
            $("#abilitiesPoke").html(hslABS);
            hslstance = "";
            $.each(result.moves, function (key, val) {
                hslstance += `<span class="badge badge-secondary">${val.move.name}</span> `;
            })
            $("#stancePoke").html(hslstance);
            hsltype = "";
            $.each(result.types, function (key, val) {
                hsltype += `<span class="badge badge-success">${val.type.name}</span> `;
            })
            $("#typePoke").html(hsltype);
            hslstatic = "";
            $.each(result.stats, function (key, val) {
                hslstatic += `<p class="font-weight-normal text-capitalize m-1" style="margin-bottom:-1px;">${val.stat.name}</p>
                    <div class="progress">
                        <div class="progress-bar bg-info" role="progressbar" style="width: ${val.base_stat}%" aria-valuenow="${val.base_stat}" aria-valuemin="0" aria-valuemax="100">${val.base_stat}%</div>
                    </div> `;
            })
            $("#static").html(hslstatic);

            //$.each(result.results, function (key, val) {
            //    //text += "<li>" + val.name + "</li>";
               
            //})
        }).fail((error) => {
            console.log(error);
        });
        // As pointed out in comments, 
        // it is unnecessary to have to manually call the modal.
        // $('#addBookDialog').modal('show');
    });
});