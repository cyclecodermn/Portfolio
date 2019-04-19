$(document).ready(function () {

    loadContacts();
    //search-button
    // Add Button onclick handler

        $('#search-button').click(function (event) {

            // we need to clear the previous content so we don't append to it
            clearContactTable();
            $('#addFormDiv').show();
            // grab the the tbody element that will hold the rows of contact information
            var contentRows = $('#contentRows');
            
            var searchType = $('#search-type').val();
            var searchText = $('#search-term').val();
            $('#search-type').empty();
            $('#search-term').empty();

            var searchURL='http://localhost:8080/dvds/'+searchType+'/'+searchText;
            
            $.ajax ({
                type: 'GET',
                url: searchURL,
                success: function (data, status) {
                    $.each(data, function (index, contact) {
                        var name = contact.title;
                        var director = contact.director;
                        var id = contact.dvdId;
                        var releaseYear = contact.realeaseYear;
                        var rating=contact.rating;
                        var row = '<tr>';
                            row += '<td>' + name + '</td>';
                            row += '<td>' + releaseYear + '</td>';
                            row += '<td>' + director + '</td>';
                            row += '<td>' + rating + '</td>';
                            row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                            row += '<td><a onclick="deleteContact(' + id + ')">Delete</a></td>';
                            row += '</tr>';
                        contentRows.append(row);
                    });
                },
                error: function() {
                    $('#errorMessages')
                        .append($('<li>')
                        .attr({class: 'list-group-item list-group-item-danger'})
                        .text('Error:'));
                }
            });


        });

        $('#edit-cancel-button').click(function (event)
        {
        // id="edit-cancel-button"
        // class="btn btn-default">
        hideEditForm();    
        $('#addFormDiv').show();

        
    });

    $('#add-button').click(function (event) {
        // check for errors and display any that we have
        // pass the input associated with the add form to the validation function
        var haveValidationErrors = checkAndDisplayValidationErrors($('#add-form').find('input'));

        // if we have errors, bail out by returning false
        if (haveValidationErrors) {
            return false;
        }

        var dvdYear=$("#add-year").val();
        // if we made it here, there are no errors so make the ajax call
        $.ajax({
            type: 'POST',
            url: 'http://localhost:8080/dvd',
            data: JSON.stringify({
                title: $('#add-title').val(),
                realeaseYear: dvdYear,
                director: $('#add-director').val(),
                rating: $('#add-rating').val(),
                notes: $('#add-notes').val()
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function(data, status) {
                // clear errorMessages
                $('#errorMessages').empty();
               // Clear the form and reload the table
                $('#add-title').val('');
                $('#add-year').val('');
                $('#add-director').val('');
                $('#add-rating').val('');
                $('#add-notes').val('');
                loadContacts();
            },
            error: function() {
                $('#errorMessages')
                   .append($('<li>')
                   .attr({class: 'list-group-item list-group-item-danger'})
                   .text('Error: '+status));
            }
        });
    });

    // Update Button onclick handler
    $('#edit-button').click(function (event) {

        // check for errors and display any that we have
        // pass the input associated with the edit form to the validation function
        var haveValidationErrors = checkAndDisplayValidationErrors($('#edit-form').find('input'));

        // if we have errors, bail out by returning false
        if (haveValidationErrors) {
            return false;
        }

        // if we get to here, there were no errors, so make the Ajax call
        $.ajax({
           type: 'PUT',
           url: 'http://localhost:8080/dvd/' + $('#edit-dvd-id').val(),
           data: JSON.stringify({
             dvdId: $('#edit-dvd-id').val(),
             title: $('#edit-title').val(),
             realeaseYear: $('#edit-year').val(),
             director: $('#edit-director').val(),
             notes: $('#edit-notes').val(),
             rating: $('#edit-rating').val()
           }),
           headers: {
             'Accept': 'application/json',
             'Content-Type': 'application/json'
           },
           'dataType': 'json',
            success: function() {
                // clear errorMessages
                $('#errorMessages').empty();
                hideEditForm();
                loadContacts();
           },
           error: function() {
             $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error calling web service.  Please try again later.'));
           }
       })
    });
});

function loadContacts() {
    // we need to clear the previous content so we don't append to it
    clearContactTable();
    $('#addFormDiv').show();
    //alert('reached loadContacts');
    // grab the the tbody element that will hold the rows of contact information
    var contentRows = $('#contentRows');

    $.ajax ({
        type: 'GET',
        url: 'http://localhost:8080/dvds',
        success: function (data, status) {
            $.each(data, function (index, contact) {
                var name = contact.title;
                var director = contact.director;
                var id = contact.dvdId;
                var releaseYear = contact.realeaseYear;
                var rating = contact.rating;

                var row = '<tr>';
                    row += '<td>' + name + '</td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="deleteContact(' + id + ')">Delete</a></td>';
                    row += '</tr>';
                contentRows.append(row);
            });
        },
        error: function() {
            $('#errorMessages')
                .append($('<li>')
                .attr({class: 'list-group-item list-group-item-danger'})
                .text('Error:'));
        }
    });
}

//function SearchContacts() 

function clearContactTable() {
    $('#contentRows').empty();
}

function showEditForm(dvdId) {
    hideAddForm();
    // clear errorMessages
    $('#errorMessages').empty();
    // get the contact details from the server and then fill and show the
    // form on success
    $.ajax({
        type: 'GET',
        url: 'http://localhost:8080/dvd/' + dvdId,
        success: function(data, status) {
              $('#edit-title').val(data.title);
              $('#edit-year').val(data.realeaseYear);
              $('#edit-director').val(data.director);
              $('#edit-notes').val(data.notes);
              $('#edit-rating').val(data.rating);
              $('#edit-dvd-id').val(data.dvdId);
          },
          error: function() {
            $('#errorMessages')
               .append($('<li>')
               .attr({class: 'list-group-item list-group-item-danger'})
               .text('Error calling web service.  Please try again later.'));
          }
    });
    $('#contactTableDiv').hide();
    $('#editFormDiv').show();
}

function hideEditForm() {
    // clear errorMessages
    $('#errorMessages').empty();
    // clear the form and then hide it
    $('#edit-title').val('');
    $('#edit-year').val('');
    $('#edit-director').val('');
    $('#edit-rating').val('');
    $('#edit-notes').val('');
    $('#editFormDiv').hide();
    $('#contactTableDiv').show();
}

function hideAddForm() {
    // clear errorMessages
    $('#errorMessages').empty();
    // clear the form and then hide it
    $('#add-title').val('');
    $('#add-year').val('');
    $('#add-director').val('');
    $('#add-rating').val('');
    $('#add-notes').val('');
    $('#addFormDiv').hide();
    $('#contactTableDiv').show();
}

function deleteContact(dvdId) {
    $.ajax ({
        type: 'DELETE',
        url: "http://localhost:8080/dvd/" + dvdId,
        success: function (status) {
            loadContacts();
        }
    });
}

// processes validation errors for the given input.  returns true if there
// are validation errors, false otherwise
function checkAndDisplayValidationErrors(input) {
    // clear displayed error message if there are any
    $('#errorMessages').empty();
    // check for HTML5 validation errors and process/display appropriately
    // a place to hold error messages
    var errorMessages = [];

    // loop through each input and check for validation errors
    input.each(function() {
        // Use the HTML5 validation API to find the validation errors
        if(!this.validity.valid)
        {
            var errorField = $('label[for='+this.id+']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    // put any error messages in the errorMessages div
    if (errorMessages.length > 0){
        $.each(errorMessages,function(index,message){
            $('#errorMessages').append($('<li>').attr({class: 'list-group-item list-group-item-danger'}).text(message));
        });
        // return true, indicating that there were errors
        return true;
    } else {
        // return false, indicating that there were no errors
        return false;
    }
}
