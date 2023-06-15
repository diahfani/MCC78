/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon",
    type: 'GET',
}).done((result) => {
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += `<tr>
            <td>${key + 1}</td>
            <td>${val.name}</td>
            <td><button type="button" onclick="getDetail('${val.url}')" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Detail</button></td>
            </tr>`
    })
    *//*$(".modal-title").html(val.name);*//*
    $("tbody").append(temp);
    console.log(result)
})*/

function getDetail(Url) {
    $.ajax({
        url: Url,
        type: 'GET',
    }).done((result) => {
        console.log(result);
        $("#imgPoke").attr("src", result.sprites.other['official-artwork'].front_default);
        $("#imgPoke").attr("width", 400);
        let temp = "";
        $.each(result.types, (key, val) => {
            const badgeColor = {
                grass: "success",
                fire: "danger",
                poison: "warning",
                normal: "secondary",
                flying: "dark",
                water: "primary",
                bug: "info",
            }[val.type.name] || "light text-dark";
            temp += `<span class="badge bg-${badgeColor} badge-width">${val.type.name}</span>`
        })
        $("#rowBadge").html(temp);
        $(".name-pokemon").html(result.name)

        $("#hp").attr({
            value: result.stats[0].base_stat,
            style: `--value: ${result.stats[0].base_stat}; --max: 200;`
        })
        $("#attack").attr({
            value: result.stats[1].base_stat,
            style: `--value: ${result.stats[1].base_stat}; --max: 200;`
        })
        $("#defense").attr({
            value: result.stats[2].base_stat,
            style: `--value: ${result.stats[2].base_stat}; --max: 200;`
        })
        $("#specialAttack").attr({
            value: result.stats[3].base_stat,
            style: `--value: ${result.stats[3].base_stat}; --max: 200;`
        })
        $("#specialDefense").attr({
            value: result.stats[4].base_stat,
            style: `--value: ${result.stats[4].base_stat}; --max: 200;`
        })
        $("#speed").attr({
            value: result.stats[5].base_stat,
            style: `--value: ${result.stats[5].base_stat}; --max: 200;`
        })
        let abilities = ""
        let arrayUrl = [{}]

        $.each(result.abilities, (key,val) => {
            abilities += `<h5 class="name-abilities mt-2" id="name${key}">${val.ability.name}</h5>`
            arrayUrl.push({
                "key": key,
                "url": val.ability.url,
                "name": val.ability.name
            })

        })
        arrayUrl.shift()
        $(".abilities-0").html(abilities);
        getAbilitiesDetail(arrayUrl)
        getDescription(result.species.url)
        $(".base-experience").html(`Base Experience : ${result.base_experience}`)
        $(".height").html(`Height : ${ result.height }`)
        $(".weight").html(`Weight :${result.weight}`)
    })
}

function getAbilitiesDetail(arrayUrl) {
    $.each(arrayUrl, (key, val) => {

        $.ajax({
            url: val.url,
            type: 'GET',
        }).done((result) => {
            $.each(result.effect_entries, (key1, val) => {
            if (val.language.name == 'en') {
                $(`<p class="desc-abilities" id="desc${key}">${val.effect}</p>`).insertAfter(`#name${key}`)
            }
        })
        })
    })
}

function getDescription(url) {
    /*console.log(url)*/
    $.ajax({
        url: url,
        type: 'GET',
    }).done((result) => {
        console.log(result)
        $.each(result.flavor_text_entries, (key, val) => {
            if (val.language.name == 'en') {
                console.log(val.flavor_text)
            $(".description").html(val.flavor_text)
            }
        })
    })
}

function employeeValidation() {
    const regexNIK = /^(1[1-9]|21|[37][1-6]|5[1-3]|6[1-5]|[89][12])\d{2}\d{2}([04][1-9]|[1256][0-9]|[37][01])(0[1-9]|1[0-2])\d{2}\d{4}$/
    const regexSymbol = /^[^<>]+$/
    const regexEmail = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/
    const regexPhoneNumber = /^(\+62|62|0)8[1-9][0-9]{6,9}$/
    $("#employeeNIK").on("change", function () {
        let valueNIK = $("#employeeNIK").val()
        console.log(valueNIK)
        if (valueNIK != "") {
            $("#employeeNIK").removeClass("is-invalid")
            $("#employeeNIK").addClass("is-valid")
            $("#validationNIK").html(`
            <div class="valid-feedback d-flex mb-1" id="validNIK">
                Looks good!
            </div>`)
        } else {
            $("#employeeNIK").removeClass("is-valid")
            $("#employeeNIK").addClass("is-invalid")
            $("#validationNIK").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidNIK">
                NIK invalid. NIK must be 16 digits!
            </div>`)
        }
    })
    $("#employeeFirstName").on("change", function () {
        let valueFirstName = $("#employeeFirstName").val()
        console.log(valueFirstName)
        if (valueFirstName != "") {
            $("#employeeFirstName").removeClass("is-invalid")
            $("#employeeFirstName").addClass("is-valid")
            $("#validationFirstName").html(`
            <div class="valid-feedback d-flex mb-1" id="validFirstName">
                Looks good!
            </div>`)
        } else {
            $("#employeeFirstName").removeClass("is-valid")
            $("#employeeFirstName").addClass("is-invalid")
            $("#validationFirstName").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidFirstName">
                First Name invalid! First Name must be filled
            </div>`)
        }
    })
    $("#employeeLastName").on("change", function () {
        let valueLastName = $("#employeeLastName").val()
        console.log(valueLastName)
        if (valueLastName != "") {
            $("#employeeLastName").removeClass("is-invalid")
            $("#employeeLastName").addClass("is-valid")
            $("#validationLastName").html(`
            <div class="valid-feedback d-flex mb-1" id="validLastName">
                Looks good!
            </div>`)
        } else {
            $("#employeeLastName").removeClass("is-valid")
            $("#employeeLastName").addClass("is-invalid")
            $("#validationLastName").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidLastName">
                Last Name invalid! Last Name must be filled
            </div>`)
        }
    })

    $("#employeeBirthdate").on("change", function () {
        let valueBirthdate = $("#employeeBirthdate").val()
        console.log(valueBirthdate)
        if (valueBirthdate != "") {
            $("#employeeBirthdate").removeClass("is-invalid")
            $("#employeeBirthdate").addClass("is-valid")
            $("#validationBirthdate").html(`
            <div class="valid-feedback d-flex mb-1" id="validBirthdate">
                Looks good!
            </div>`)
        } else {
            $("#employeeBirthdate").removeClass("is-valid")
            $("#employeeBirthdate").addClass("is-invalid")
            $("#validationBirthdate").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidBirthdate">
                Birthdate invalid!
            </div>`)
        }
    })

    $("#employeeHiringdate").on("change", function () {
        let valueHiringdate = $("#employeeHiringdate").val()
        console.log(valueHiringdate)
        if (valueHiringdate != "") {
            $("#employeeHiringdate").removeClass("is-invalid")
            $("#employeeHiringdate").addClass("is-valid")
            $("#validationHiringdate").html(`
            <div class="valid-feedback d-flex mb-1" id="validHiringdate">
                Looks good!
            </div>`)
        } else {
            $("#employeeHiringdate").removeClass("is-valid")
            $("#employeeHiringdate").addClass("is-invalid")
            $("#validationHiringdate").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidHiringdate">
                Hiring date invalid!
            </div>`)
        }
    })

    $("input[name=radioGender]").on("change", function () {
        let valueGender = $("input[name=radioGender]:checked").val();
        console.log(valueGender)
    })

    // validation gender
    /*var radioButtons = document.getElementsByName("radioGender");
    var isChecked = false;

    for (var i = 0; i < radioButtons.length; i++) {
        if (radioButtons[i].checked) {
            isChecked = true;
            break;
        }
    }

    if (!isChecked) {
        alert("Please select an option");
    } else {
       
    }*/

    $("#employeeEmail").on("change", function () {
        let valueEmail = $("#employeeEmail").val()
        console.log(valueEmail)
        if (regexEmail.test(valueEmail) && valueEmail != "") {
            $("#employeeEmail").removeClass("is-invalid")
            $("#employeeEmail").addClass("is-valid")
            $("#validationEmail").html(`
            <div class="valid-feedback d-flex mb-1" id="validEmail">
                Looks good!
            </div>`)
        } else {
            $("#employeeEmail").removeClass("is-valid")
            $("#employeeEmail").addClass("is-invalid")
            $("#validationEmail").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidEmail">
                Email invalid!
            </div>`)
        }
    })

    $("#employeePhoneNumber").on("change", function () {
        let valuePhoneNumber = $("#employeePhoneNumber").val()
        console.log(valuePhoneNumber)
        if (valuePhoneNumber != "") {
            $("#employeePhoneNumber").removeClass("is-invalid")
            $("#employeePhoneNumber").addClass("is-valid")
            $("#validationPhoneNumber").html(`
            <div class="valid-feedback d-flex mb-1" id="validPhoneNumber">
                Looks good!
            </div>`)
        } else {
            $("#employeePhoneNumber").removeClass("is-valid")
            $("#employeePhoneNumber").addClass("is-invalid")
            $("#validationPhoneNumber").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidPhoneNumber">
                Phone number invalid! Phone number must be 10-13 digits!
            </div>`)
        }
    })

    /*$("#employeeUniversity").on("change", function () {
        let valueUniversity = $("#employeeUniversity").val()
        if (valueUniversity != "") {
            $("#employeeUniversity").removeClass("is-invalid")
            $("#employeeUniversity").addClass("is-valid")
            $("#validationUniversity").html(`
            <div class="valid-feedback d-flex mb-1" id="validUniversity">
                Looks good!
            </div>`)
        } else {
            $("#employeeUniversity").removeClass("is-valid")
            $("#employeeUniversity").addClass("is-invalid")
            $("#validationUniversity").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidUniversity">
                University invalid!
            </div>`)
        }
    })

    $("#employeeMajor").on("change", function () {
        let valueMajor = $("#employeeMajor").val()
        if (valueMajor != "") {
            $("#employeeMajor").removeClass("is-invalid")
            $("#employeeMajor").addClass("is-valid")
            $("#validationMajor").html(`
            <div class="valid-feedback d-flex mb-1" id="validMajor">
                Looks good!
            </div>`)
        } else {
            $("#employeeMajor").removeClass("is-valid")
            $("#employeeMajor").addClass("is-invalid")
            $("#validationMajor").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidMajor">
                Major invalid!
            </div>`)
        }
    })

    $("#employeeGPA").on("change", function () {
        let valueGPA = $("#employeeGPA").val()
        if (valueGPA != "") {
            $("#employeeGPA").removeClass("is-invalid")
            $("#employeeGPA").addClass("is-valid")
            $("#validationGPA").html(`
            <div class="valid-feedback d-flex mb-1" id="validGPA">
                Looks good!
            </div>`)
        } else {
            $("#employeeGPA").removeClass("is-valid")
            $("#employeeGPA").addClass("is-invalid")
            $("#validationGPA").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidGPA">
                GPA invalid!
            </div>`)
        }
    })

    $("#employeeDepartemen").on("change", function () {
        let valueDepartment = $("#employeeDepartemen").val()
        if (valueDepartment != "") {
            $("#employeeDepartemen").removeClass("is-invalid")
            $("#employeeDepartemen").addClass("is-valid")
            $("#validationDepartmen").html(`
            <div class="valid-feedback d-flex mb-1" id="validDepartmen">
                Looks good!
            </div>`)
        } else {
            $("#employeeDepartemen").removeClass("is-valid")
            $("#employeeDepartemen").addClass("is-invalid")
            $("#validationDepartmen").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidDepartmen">
                Departmen invalid!
            </div>`)
        }
    })

    $("#employeeDegree").on("change", function () {
        let valueDegree = $("#employeeDegree").val()
        if (valueDegree != "") {
            $("#employeeDegree").removeClass("is-invalid")
            $("#employeeDegree").addClass("is-valid")
            $("#validationDegree").html(`
            <div class="valid-feedback d-flex mb-1" id="validDegree">
                Looks good!
            </div>`)
        } else {
            $("#employeeDegree").removeClass("is-valid")
            $("#employeeDegree").addClass("is-invalid")
            $("#validationDegree").html(`
            <div class="invalid-feedback d-flex mb-1" id="invalidDegree">
                Degree invalid!
            </div>`)
        }
    })
*/
}

function submitEmployee() {
    /*$("#saveEmployee").on("submit", function (event) {*/
        event.preventDefault();
        let eNik = $('#employeeNIK').val();
        let eFirst = $('#employeeFirstName').val();
        let eLast = $('#employeeLastName').val();
        let eBDate = $('#employeeBirthdate').val();
        var eGender = document.querySelector('input[name="radioGender"]:checked').id.includes('m') ? 1 : 0;
        let eHDate = $('#employeeHiringdate').val();
        let eEmail = $('#employeeEmail').val();
        let ePhone = $('#employeePhoneNumber').val();

        $.ajax({
            async: true, // Async by default is set to “true” load the script asynchronously  
            // URL to post data into sharepoint list  
            url: "https://localhost:7159/api/Employee",
            method: 'POST', //Specifies the operation to create the list item  
            data: JSON.stringify({
                /*'__metadata': {
                    'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
                },*/
                //Pass the parameters
                'nik': eNik,
                'firstName': eFirst,
                'lastName': eLast,
                'birthDate': eBDate,
                'gender': eGender,
                'hiringDate': eHDate,
                'email': eEmail,
                'phoneNumber': ePhone
            }),
            headers: {
                "accept": "application/json;odata=verbose", //It defines the Data format   
                "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
                /*"X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value */  
            },
            success: function (data, event) {
                console.log(data);
            },
            error: function (error, event, data) {
                console.log(data)
                console.log(JSON.stringify(error));
            }

        })

    /*})*/
}



function insertEmployee() {
    $("#saveEmployee").on("click", function (event) {
        event.preventDefault();
        var eNik = document.getElementById('employeeNIK').value;
        var eFirst = document.getElementById('employeeFirstName').value;
        var eLast = document.getElementById('employeeLastName').value;
        var eBDate = document.getElementById('employeeBirthdate').value;
        var eGender = document.querySelector('input[name="radioGender"]:checked').id.includes('lakilakiEditGender') ? 1 : 0;
        var eHDate = document.getElementById('employeeHiringdate').value;
        var eEmail = document.getElementById('employeeEmail').value;
        var ePhone = document.getElementById('employeePhoneNumber').value;

        $.ajax({
            async: true, // Async by default is set to “true” load the script asynchronously  
            // URL to post data into sharepoint list  
            url: "https://localhost:7159/api/Employee",
            method: 'POST', //Specifies the operation to create the list item  
            data: JSON.stringify({
                '__metadata': {
                    'type': 'SP.Data.EmployeeListItem' // it defines the ListEnitityTypeName  
                },
                //Pass the parameters
                'nik': eNik,
                'firstName': eFirst,
                'lastName': eLast,
                'birthDate': eBDate,
                'gender': eGender,
                'hiringDate': eHDate,
                'email': eEmail,
                'phoneNumber': ePhone
            }),
            headers: {
                "accept": "application/json;odata=verbose", //It defines the Data format   
                "content-type": "application/json;odata=verbose", //It defines the content type as JSON  
/*                "X-RequestDigest": $("#__REQUESTDIGEST").val() //It gets the digest value   
*/            },
            success: function (data, event) {
                console.log(data);
            },
            error: function (error, event) {
                console.log(JSON.stringify(error));
                
            }

        })

    })

}

function deleteEmployee(guid) {
    console.log(guid)
    $.ajax({
        url: `https://localhost:7159/api/Employee/${guid}`,
        type: 'DELETE',
        success: function (result) {
            console.log(result);
            window.location.reload()
        }, error: function (xhr, status, error) {
            console.error('error occured: ', error)
        }
    })
}

function openModalEditEmployee(guid, nik, firstName, lastName, birthDate, gender, hiringDate, email, phoneNumber) {
    /*$('#employeeModal #guid').val(guid);*/
    $('#employeeEditNIK').val(nik);
    $('#employeeEditFirstName').val(firstName);
    $('#employeeEditLastName').val(lastName);
    $('#employeeEditBirthdate').val(birthDate);
    $('#employeeEditHiringdate').val(hiringDate);
    /*$('#employeeEdit').val(email);*/
    $('#employeeEditEmail').val(email);
    $('#employeeEditPhoneNumber').val(phoneNumber);

    if (gender == 0) {
        $("#perempuanEditGender").prop("checked", true);
    } else {
        $("#lakilakiEditGender").prop("checked", true);
    }

    $("#editEmployee").on("click", function () {
        event.preventDefault();
        editEmployee(guid);
    })

/*    console.log(guid)
    console.log(nik)
    console.log(firstName)
    console.log(lastName)
    console.log(birthDate)
    console.log(gender)
    console.log(hiringDate)
    console.log(email)
    console.log(phoneNumber)*/
}

function editEmployee(guid) {
    let editNik = document.getElementById('employeeEditNIK').value;
    let editFirst = document.getElementById('employeeEditFirstName').value;
    let editLast = document.getElementById('employeeEditLastName').value;
    let editBDate = document.getElementById('employeeEditBirthdate').value;
    let editGender = document.querySelector('input[name="radioEditGender"]:checked').id.includes('lakilakiEditGender') ? 1 : 0;
    let editHDate = document.getElementById('employeeEditHiringdate').value;
    let editEmail = document.getElementById('employeeEditEmail').value;
    let editPhone = document.getElementById('employeeEditPhoneNumber').value;

    $.ajax({
        url: `https://localhost:7159/api/Employee`,
        method: "PUT",
        data: JSON.stringify({
            /*'__metadata': {
                'type': 'SP.Data.EmployeeListItem'
            },*/
            'guid': guid,
            'nik': editNik,
            'firstName': editFirst,
            'lastName': editLast,
            'birthDate': editBDate,
            'gender': editGender,
            'hiringDate': editHDate,
            'email': editEmail,
            'phoneNumber': editPhone
        }),
        headers: {
            "accept": "application/json;odata=verbose",
            "content-type": "application/json;odata=verbose",
            "X-HTTP-Method": "MERGE",
            "IF-MATCH": "*"
        },
        success: function (data) {
            console.log(data);
            window.location.reload()
            // Get the updated row data as an array
            var updatedRowData = [
                editNik,
                editFirst,
                editLast,
                editBDate,
                editGender == 1 ? 'Male' : 'Female',
                editHDate,
                editEmail,
                editPhone
                // Add other columns as needed
            ];

            // Loop through each row in the DataTable
            dataTable.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var rowData = this.data();

                // Check if the GUID in the row matches the guid parameter
                if (rowData[0] === guid) {
                    // Update the row data
                    this.data(updatedRowData);

                    // Redraw the updated row
                    this.invalidate();

                    // Exit the loop
                    return false;
                }
            });

            // Close the modal
            $('#employeeEditModal').modal('hide');
        },
        error: function (error) {
            console.log("Error: " + JSON.stringify(error));
        }
    });
}


$(document).ready(function () {
    $("#tableEmployee").DataTable({
        ajax: {
            url: "https://localhost:7159/api/Employee",
            dataSrc: 'data',
            dataType: 'JSON'
        },
        dom : 'Bfrtip',
        buttons: [
            'copy', 'excel', 'pdf', 'print'
        ],
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: 'guid'
            },
            { data: 'nik' },
            { data: 'firstName' },
            { data: 'lastName' },
            { data: 'birthDate' },
            {
                data: 'gender',
                render: function (data) {
                    return data == 0 ? 'Perempuan' : 'Laki-laki'
                }
            },
            { data: 'hiringDate' },
            { data: 'email' },
            { data: 'phoneNumber' },
            /*{
                data: 'url',
                render: function (data, type, row, meta) {
                    return `<button type="button" onclick="getDetail('${data}')" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">Detail</button>`
                }   
            },*/
            {
                render: function (data, type, row, meta) {
                    return `<button type="button" class="btn btn-outline-warning" onclick="openModalEditEmployee('${row.guid}', '${row.nik}', '${row.firstName}', '${row.lastName}', '${row.birthDate}', '${row.gender}', '${row.hiringDate}', '${row.email}', '${row.phoneNumber}')" data-bs-toggle="modal" data-bs-target="#employeeEditModal">Edit</button>
                    <button type="button" class="btn btn-outline-danger" onclick="deleteEmployee('${row.guid}')" id="${row.guid}">Delete</button>`
                }
            }
        ]
    });
})