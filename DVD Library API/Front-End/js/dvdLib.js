$(document).ready(function () {
    hideAddForm();
    loadAllDVDs();
    //search-button
    // Add Button onclick handler

    $('#reset-button').click(function (event) {
        hideAddForm();
        hideEditForm();
        $('#search-term').val('');
        showSearchResultList()
        loadAllDVDs();
    });
        
    $('#addDVD-button').click(function (event) {
        hideEditForm();
        hideSearchResultList();
        $('#search-term').val('');
        showAddForm();
    });
    
    $('#search-button').click(function (event) {

        hideAddForm();
        hideEditForm();
        // clear the previous content so we don't append to it
        cleardvdTable();
        // $('#addFormDiv').show();
        // grab the the tbody element that will hold the rows of contact information
        var contentRows = $('#contentRows');
        
        var searchType = $('#search-type').val();
        var searchText = $('#search-term').val();
        //$('#search-type').empty();
        $('#search-term').text('');

        var searchURL='http://localhost:63001/dvds/'+searchType+'/'+searchText;
        
        $.ajax ({
            type: 'GET',
            url: searchURL,
            success: function (data, status) {
                $.each(data, function (index, oneDvd) {
                    var name = oneDvd.title;
                    var director = oneDvd.director;
                    var id = oneDvd.dvdId;
                    var releaseYear = oneDvd.realeaseYear;
                    var rating=oneDvd.rating;
                    var row = '<tr>';
                        row += '<td>' + name + '</td>';
                        row += '<td>' + releaseYear + '</td>';
                        row += '<td>' + director + '</td>';
                        row += '<td>' + rating + '</td>';
                        row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                        row += '<td><a onclick="deleteDVD(' + id + ')">Delete</a></td>';
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
        hideAddForm();
        hideEditForm();    
    });

    $('#add-cancel-button').click(function (event)
    {
        // id="edit-cancel-button"
        // class="btn btn-default">
        hideAddForm();
        hideEditForm();    
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
            url: 'http://localhost:63001/dvd',
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
                loadAllDVDs();
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
           url: 'http://localhost:63001/dvd/' + $('#edit-dvd-id').val(),
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
                loadAllDVDs();
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

function loadAllDVDs() {
    // we need to clear the previous content so we don't append to it
    cleardvdTable();
    //$('#addFormDiv').show();
    //alert('reached loadAllDVDs');
    // grab the the tbody element that will hold the rows of contact information
    var contentRows = $('#contentRows');

    $.ajax ({
        type: 'GET',
        url: 'http://localhost:63001/dvds',
        success: function (data, status) {
            $.each(data, function (index, oneDvd) {
                var name = oneDvd.title;
                var director = oneDvd.director;
                var id = oneDvd.dvdId;
                var releaseYear = oneDvd.realeaseYear;
                var rating = oneDvd.rating;

                var row = '<tr>';
                    row += '<td>' + name + '</td>';
                    row += '<td>' + releaseYear + '</td>';
                    row += '<td>' + director + '</td>';
                    row += '<td>' + rating + '</td>';
                    row += '<td><a onclick="showEditForm(' + id + ')">Edit</a></td>';
                    row += '<td><a onclick="deleteDVD(' + id + ')">Delete</a></td>';
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

function cleardvdTable() {
    $('#contentRows').empty();
}

function showEditForm(dvdId) {
    hideAddForm();
    $('#search-term').val('');
    // clear errorMessages
    $('#errorMessages').empty();
    // get the contact details from the server and then fill and show the
    // form on success
    $.ajax({
        type: 'GET',
        url: 'http://localhost:63001/dvd/' + dvdId,
        success: function(data, status) {

              $('#edit-title').val(data.title);
              $('#edit-year').val(data.realeaseYear);
              $('#edit-director').val(data.director);
              $('#edit-notes').val(data.notes);
              $('#edit-rating').val($.trim(data.rating));
              $('#edit-dvd-id').val(data.dvdId);
          },
          error: function() {
            $('#errorMessages')
               .append($('<li>')
               .attr({class: 'list-group-item list-group-item-danger'})
               .text('Error calling web service.  Please try again later.'));
          }
    });
    $('#dvdTableDiv').hide();
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
    $('#dvdTableDiv').show();
}

function showAddForm() {
    //alert ('Reached hide Add Form');
  
        $('#addFormDiv').show();
    }

function hideSearchResultList() {
    $('#dvdTableDiv').hide();
    }

function showSearchResultList() {
    $('#dvdTableDiv').show();
    }
    
function hideAddForm() {
//alert ('Reached hide Add Form');

    // clear errorMessages
    $('#errorMessages').empty();
    // clear the form and then hide it
    $('#add-title').val('');
    $('#add-year').val('');
    $('#add-director').val('');
    $('#add-rating').val('');
    $('#add-notes').val('');
    $('#addFormDiv').hide();
    $('#dvdTableDiv').show();
}

function deleteDVD(dvdId) {
    $.ajax ({
        type: 'DELETE',
        url: "http://localhost:63001/dvd/" + dvdId,
        success: function (status) {
            loadAllDVDs();
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
