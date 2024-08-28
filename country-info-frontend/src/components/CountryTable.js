import React from 'react';

// CountryTable component to display a table of countries
const CountryTable = ({ countries, handleCountryClick, handleRegionClick, handleSubregionClick }) => {
    return (
        <table className="country-table">
            <thead>
                <tr>
                    <th>Name</th>
                    <th>Region</th>
                    <th>Subregion</th>
                </tr>
            </thead>
            <tbody>
                {countries.map(country => (
                    <tr key={country.cca3}>
                        <td onClick={() => handleCountryClick(country.name.common, 'name')}>
                            {country.name.common}
                        </td>
                        <td onClick={() => handleRegionClick(country.region)}>
                            {country.region}
                        </td>
                        <td onClick={() => handleSubregionClick(country.subregion)}>
                            {country.subregion}
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

export default CountryTable;