
@section Scripts 
{
    <script>
        $(document).ready(function () {
            $("#searchForm").submit(function (e) {
                search();
                return false;
            });
        });

        function search() {
            var params;
            var imagePath = '@Url.Content("~/Images/")';
            var detailsPath = '@Url.Action("Details", "Bikes")/';				
				
					params = 'minPrice=' + $('#minPrice').val() + 'maxPrice=' + $('#maxPrice').val() + 'minYear=' + $('#minYear').val() + 'maxYear=' + $('#maxYear').val() + makeModelOrYr;

            $.ajax({
                type: 'GET',
                url: 'http://localhost:64332/api/bikes/search?' + params,
                success: function (results) {
                    $('#searchResults').empty();

                    $.each(results, function (index, bikeResults)


					{
                        var html = '<div class="col-xs-12 col-sm-6 col-md-4">' +
                            '<p class="bikeListing"><img src="'+ imagePath + bikeResults.ImageFileName + '" /></p>' +
                        '<p class="bikeListing"><strong>' + bikeResults.BikeModel + ', ' + bikeResults.BikeMake + '</strong></p>' +
                        '<p class="bikeListing">' + bikeResults.BikeYear + 
                        '<p class="bikeListing"><a href="' + detailsPath + bikeResults.BikeId + '">view details</a></p>' +
                        '</div>';

                        $('#searchResults').append(html.toString());
                    });
										
					{
                        var html = '<div class="col-xs-12 col-sm-6 col-md-4">' +
                            '<p class="recentListing"><img src="'+ imagePath + listing.ImageFileName + '" /></p>' +
                        '<p class="recentListing"><strong>' + listing.City + ', ' + listing.StateId + '</strong></p>' +
                        '<p class="recentListing">' + listing.Rate + ' / night</p>' +
                        '<p class="recentListing"><a href="' + detailsPath + listing.ListingId + '">view details</a></p>' +
                        '</div>';

                        $('#searchResults').append(html.toString());
                    });

                },
                error: function () {
                    alert('Error performing search, try again later!')
                }
            });
        }
    </script>