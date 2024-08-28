import React from 'react';

// RecordsPerPageSelector component to select the number of records per page
const RecordsPerPageSelector = ({ countriesPerPage, handleRecordsPerPageChange }) => {
    return (
        <div className="records-per-page">
            <label htmlFor="recordsPerPage">Records per page: </label>
            <select id="recordsPerPage" value={countriesPerPage} onChange={handleRecordsPerPageChange}>
                <option value="50">50</option>
                <option value="100">100</option>
                <option value="150">150</option>
                <option value="200">200</option>
            </select>
        </div>
    );
};

export default RecordsPerPageSelector;