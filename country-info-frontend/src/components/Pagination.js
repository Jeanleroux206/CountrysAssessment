import React from 'react';

// Pagination component to handle pagination of the country list
const Pagination = ({ countriesPerPage, totalCountries, paginate, currentPage }) => {
    const pageNumbers = [];

    // Calculate the total number of pages
    for (let i = 1; i <= Math.ceil(totalCountries / countriesPerPage); i++) {
        pageNumbers.push(i);
    }

    return (
        <div className="pagination">
            {pageNumbers.map(number => (
                <button
                    key={number}
                    onClick={() => paginate(number)}
                    className={number === currentPage ? 'active' : ''}
                    disabled={number === currentPage}
                >
                    {number}
                </button>
            ))}
        </div>
    );
};

export default Pagination;