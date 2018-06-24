    tinymce.init({
        selector: 'textarea',
    height: 250,
    width: 700,
    menubar: false,
    plugins: [
        'advlist autolink lists link image charmap print preview pagebreak anchor textcolor',
        'searchreplace visualblocks code fullscreen',
        'insertdatetime media table contextmenu paste code help wordcount emoticons'
    ],
    toolbar: 'undo redo |  formatselect | bold italic underline  | alignleft aligncenter alignright | bullist numlist | link image media emoticons | pagebreak preview ',
    content_css: [
        '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
        '//www.tinymce.com/css/codepen.min.css']
});




function showList() {
        $.ajax({
            type: "GET",
            url: "/Contacts/ContactList",           
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            
            success: function (response) {        
                 var tbl_len = response.length;
                if (tbl_len > 0) {
                    $('#displayList').append('<thead><tr><th>Contact Name</th><th>Phone</th><th>Birth date</th><th>Contact Type</th><th>Edit</th><th>Delete</th>'
                        + '</tr></thead>');
                }


                for (var i = 0; i < tbl_len; i++) {
                    contactId = response[i].contactId;
                    $('#displayList').append('<tbody><tr>'
                        + '<td>' + response[i].contactName + '</td>'
                        + '<td>' + response[i].contactPhone + '</td>'
                        + '<td>' + ToJavaScriptDate(response[i].contactBirthdate) + '</td>'
                        + '<td>' + response[i].contactType + '</td>'
                        + '<td width="30px"><a class="square-btn" href="/Contacts/Edit/'+contactId+ '">Edit</a></td>'
                        + '<td width="30px"><a class="square-btn" href="/Contacts/Delete/'+contactId+'">Delete</a></td>'

                        + '</tr></tbody>');
                    console.log(response[1].contactBirthdate);
                }
                
                
            },


            failure: function (msg) {
                alert("System can not retrive data \nPlease contact system administrator"
                    + "\nHints: Webservice didn't response.");
            }
        }, function () {
            $("#displayList").dataTable({
                searching: false,
                paging: true,
                bLengthChange: false,
                responsive: true


            });
        } );

    }
   

 
    
        $("#nameAutoComplete").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '/ContactView/AutoComplete/',
                    data: { prefix: request.term },                    
                    type: "GET",
                    contentType: "application/json",
                    dataType:"json",
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                'label': item.cname,
                                'value': item.cname,
                                'phone': item.cphone,
                                'birth_date': ToJavaScriptDate(item.cbirth_date),
                                'contact_type_id': item.ccontact_type_id
                                
                            };
                        }));
                        console.log(data);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {
                console.log(i);
                $("#name").val(i.item.value);
                $("#phone").val(i.item.phone);
                $("#birth_date").val(i.item.birth_date);
                $("#contact_type_id").val(i.item.contact_type_id);
            },
            minLength: 1
        });

function ToJavaScriptDate(date) {
    dateTime = moment(date).format("YYYY-MM-DD");
    return dateTime; 
}